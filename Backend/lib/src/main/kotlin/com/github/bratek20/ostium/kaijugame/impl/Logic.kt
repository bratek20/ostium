package com.github.bratek20.ostium.kaijugame.impl

import com.github.bratek20.ostium.carddrawing.api.CardDrawer
import com.github.bratek20.ostium.carddrawing.api.CardDrawerFactory
import com.github.bratek20.ostium.kaijugame.api.*
import com.github.bratek20.ostium.gamesmanagement.api.*
import com.github.bratek20.ostium.user.api.Username

private class HandLogic(
    private val drawer: CardDrawer
) {
    private val cards = mutableListOf(
        drawer.draw(),
        drawer.draw(),
        drawer.draw(),
        drawer.draw()
    )

    fun getState(): Hand {
        return Hand.create(cards)
    }

    fun removeCardAndGet(idx: Int): Card {
        return cards.removeAt(idx)
    }

    fun fill() {
        while (cards.size < 4) {
            cards.add(drawer.draw())
        }
    }
}

private class AttackGiverLogic(
    val type: DamageType
) {
    private var damageValue = 0

    fun getState(): AttackGiver {
        return AttackGiver.create(
            type = type,
            damageValue = damageValue
        )
    }

    fun addDamage(value: Int) {
        damageValue += value
    }

    fun clearDamageAndGet(): Int {
        val result = damageValue
        damageValue = 0
        return result
    }
}

private class PlayerSideLogic {
    private val playedCards = mutableListOf<Card>()
    private val lightGiver = AttackGiverLogic(DamageType.Light)
    private val mediumGiver = AttackGiverLogic(DamageType.Medium)
    private val heavyGiver = AttackGiverLogic(DamageType.Heavy)
    private var focusLeft = 2

    fun getState(): PlayerSide {
        return PlayerSide.create(
            pool = AttackPool.create(
                lightGiver = lightGiver.getState(),
                mediumGiver = mediumGiver.getState(),
                heavyGiver = heavyGiver.getState(),
                focusLeft = focusLeft
            ),
            playedCards = playedCards.toList()
        )
    }

    fun handleCardPlayed(card: Card) {
        playedCards.add(card)
        getGiver(card.getType()).addDamage(card.getValue())
        focusLeft -= card.getFocusCost()
    }

    fun clearDamageAndGet(damageType: DamageType): Int {
        return getGiver(damageType).clearDamageAndGet()
    }

    fun clearFocusAndGet(): Int {
        val result = focusLeft
        focusLeft = 0
        return result
    }

    fun handleNewTurn(turn: Int) {
        focusLeft = 2 * turn
    }

    private fun getGiver(damageType: DamageType): AttackGiverLogic {
        return allGivers().first { it.type == damageType }
    }

    private fun allGivers(): List<AttackGiverLogic> {
        return listOf(lightGiver, mediumGiver, heavyGiver)
    }

    companion object {
        val initState = PlayerSideLogic().getState()
    }
}

private class PlayerStateLogic(
    drawer: CardDrawer
) {
    private val hand = HandLogic(drawer)
    private val side = PlayerSideLogic()
    private var ready = false

    private val leftZone = HitZoneLogic(HitZonePosition.Left)
    private val centerZone = HitZoneLogic(HitZonePosition.Center)
    private val rightZone = HitZoneLogic(HitZonePosition.Right)

    private lateinit var opponentRevealedTable: Table

    fun init(initialOpponentTable: Table) {
        opponentRevealedTable = initialOpponentTable
    }

    fun isReady(): Boolean {
        return ready
    }

    fun getTableState(): Table {
        return Table.create(
            leftZone = getZoneState(HitZonePosition.Left),
            centerZone = getZoneState(HitZonePosition.Center),
            rightZone = getZoneState(HitZonePosition.Right),
            mySide = getSideState(),
            opponentSide = if(::opponentRevealedTable.isInitialized) opponentRevealedTable.getMySide() else PlayerSideLogic.initState
        )
    }

    fun getSideState(): PlayerSide {
        return side.getState()
    }

    fun getHandState(): Hand {
        return hand.getState()
    }

    private fun getZoneState(zone: HitZonePosition): HitZone {
        return getHitZone(zone).getState()
    }

    private fun getHitZone(zone: HitZonePosition): HitZoneLogic {
        return when (zone) {
            HitZonePosition.Left -> leftZone
            HitZonePosition.Center -> centerZone
            HitZonePosition.Right -> rightZone
        }
    }

    fun markReady() {
        ready = true
    }

    fun clearReady() {
        ready = false
    }

    fun handleReveal(currentOpponentTable: Table) {
        leftZone.handleReveal(currentOpponentTable.getLeftZone(), opponentRevealedTable.getLeftZone())
        centerZone.handleReveal(currentOpponentTable.getCenterZone(), opponentRevealedTable.getCenterZone())
        rightZone.handleReveal(currentOpponentTable.getRightZone(), opponentRevealedTable.getRightZone())

        opponentRevealedTable = currentOpponentTable
    }

    fun handleNewTurn(turn: Int) {
        hand.fill()
        side.handleNewTurn(turn)
    }

    fun playCard(handCardIdx: Int) {
        val handCard = hand.removeCardAndGet(handCardIdx)
        side.handleCardPlayed(handCard)
    }

    fun assignDamage(zone: HitZonePosition, damageType: DamageType) {
        val damageValue = side.clearDamageAndGet(damageType)
        getHitZone(zone).assignDamage(damageType, damageValue)
    }

    fun assignGuard(zone: HitZonePosition, damageType: DamageType) {
        val guardValue = side.clearFocusAndGet()
        getHitZone(zone).assignGuard(damageType, guardValue)
    }
}

class AttackReceiverLogic(
    val type: DamageType,
) {
    private var myDamage = 0
    private var opponentDamage = 0

    fun getState(): AttackReceiver {
        return AttackReceiver.create(
            type = type,
            myDamage = myDamage,
            opponentDamage = opponentDamage
        )
    }

    fun assignDamage(value: Int) {
        myDamage += value
    }

    fun assignGuard(value: Int) {
        opponentDamage -= value
    }

    fun handleReveal(currentOpponentReceiver: AttackReceiver, previousOpponentReceiver: AttackReceiver) {
        val opponentDamageDiff = currentOpponentReceiver.getMyDamage() - previousOpponentReceiver.getMyDamage()
        opponentDamage += opponentDamageDiff

        val myDamageDiff = currentOpponentReceiver.getOpponentDamage() - previousOpponentReceiver.getOpponentDamage()
        myDamage += myDamageDiff
    }
}

private class HitZoneLogic(
    private val position: HitZonePosition
) {
    private val lightReceiver = AttackReceiverLogic(DamageType.Light)
    private val mediumReceiver = AttackReceiverLogic(DamageType.Medium)
    private val heavyReceiver = AttackReceiverLogic(DamageType.Heavy)

    fun getState(): HitZone {
        return HitZone.create(
            position = position,
            lightReceiver = lightReceiver.getState(),
            mediumReceiver = mediumReceiver.getState(),
            heavyReceiver = heavyReceiver.getState()
        )
    }

    fun assignDamage(damageType: DamageType, value: Int) {
        getReceiver(damageType).assignDamage(value)
    }

    fun assignGuard(damageType: DamageType, value: Int) {
        getReceiver(damageType).assignGuard(value)
    }

    fun handleReveal(currentOpponentZone: HitZone, previousOpponentZone: HitZone) {
        lightReceiver.handleReveal(currentOpponentZone.getLightReceiver(), previousOpponentZone.getLightReceiver())
        mediumReceiver.handleReveal(currentOpponentZone.getMediumReceiver(), previousOpponentZone.getMediumReceiver())
        heavyReceiver.handleReveal(currentOpponentZone.getHeavyReceiver(), previousOpponentZone.getHeavyReceiver())
    }

    private fun getReceiver(damageType: DamageType): AttackReceiverLogic {
        return when (damageType) {
            DamageType.Light -> lightReceiver
            DamageType.Medium -> mediumReceiver
            DamageType.Heavy -> heavyReceiver
        }
    }
}

private class GameStateLogic(
    private val creator: Username,
    drawerFactory: CardDrawerFactory
) {
    companion object {
        private val orderedPhases = listOf(
            TurnPhase.PlayCard,
            TurnPhase.AssignDamage,
            TurnPhase.AssignGuard,
            TurnPhase.Reveal
        )
    }
    private var phaseIdx = 0

    private val creatorState = PlayerStateLogic(drawerFactory.create())
    private val joinerState = PlayerStateLogic(drawerFactory.create())

    init {
        creatorState.init(joinerState.getTableState())
        joinerState.init(creatorState.getTableState())
    }

    fun getState(user: Username): GameState {
        return GameState.create(
            turn = getTurn(),
            phase = getCurrentPhase(),
            table = getMyState(user).getTableState(),
            hand = getMyState(user).getHandState(),
            myReady = getMyState(user).isReady(),
            opponentReady = getOpponentState(user).isReady()
        )
    }

    private fun getCurrentPhase(): TurnPhase {
        val adjustedIdx = phaseIdx % orderedPhases.size
        return orderedPhases[adjustedIdx]
    }

    private fun getTurn(): Int {
        return 1 + (phaseIdx / orderedPhases.size)
    }

    fun endPhase(user: Username): GameState {
        getMyState(user).markReady()
        val currentTurn = getTurn()

        if (getBothPlayers().all { it.isReady() }){
            phaseIdx++
            getBothPlayers().forEach { it.clearReady() }

            if (getCurrentPhase() == TurnPhase.Reveal) {
                val creatorTable = creatorState.getTableState()
                val joinerTable = joinerState.getTableState()

                creatorState.handleReveal(joinerTable)
                joinerState.handleReveal(creatorTable)
            }

            if (currentTurn != getTurn()) {
                getBothPlayers().forEach { it.handleNewTurn(getTurn()) }
            }
        }

        return getState(user)
    }

    private fun getBothPlayers(): List<PlayerStateLogic> {
        return listOf(creatorState, joinerState)
    }

    fun playCard(user: Username, handCardIdx: Int): GameState {
        getMyState(user).playCard(handCardIdx)
        return getState(user)
    }

    fun assignDamage(user: Username, zone: HitZonePosition, damageType: DamageType): GameState {
        getMyState(user).assignDamage(zone, damageType)
        return getState(user)
    }

    fun assignGuard(user: Username, zone: HitZonePosition, damageType: DamageType): GameState {
        getMyState(user).assignGuard(zone, damageType)
        return getState(user)
    }

    private fun getMyState(user: Username): PlayerStateLogic {
        return if (user == creator) creatorState else joinerState
    }

    private fun getOpponentState(user: Username): PlayerStateLogic {
        return if (user == creator) joinerState else creatorState
    }
}

class GameApiLogic(
    private val managementApi: GamesManagementApi,
    private val drawerFactory: CardDrawerFactory
) : GameApi {

    override fun getState(token: GameToken): GameState {
        return getGameStateLogic(token).getState(token.getUsername())
    }

    override fun endPhase(token: GameToken): GameState {
        return getGameStateLogic(token).endPhase(token.getUsername())
    }

    override fun playCard(token: GameToken, handCardIdx: Int): GameState {
        return getGameStateLogic(token).playCard(token.getUsername(), handCardIdx)
    }

    override fun assignDamage(token: GameToken, zone: HitZonePosition, damageType: DamageType): GameState {
        return getGameStateLogic(token).assignDamage(token.getUsername(), zone, damageType)
    }

    override fun assignGuard(token: GameToken, zone: HitZonePosition, damageType: DamageType): GameState {
        return getGameStateLogic(token).assignGuard(token.getUsername(), zone, damageType)
    }

    private fun getCreatedGameOrThrow(token: GameToken): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == token.getGameId() }
        if (game == null) {
            throw GameNotFoundException("Game for id ${token.getGameId()} not found");
        }
        return game
    }

    private val gameStates: MutableMap<GameId, GameStateLogic> = mutableMapOf()
    private fun getGameStateLogic(token: GameToken): GameStateLogic {
        if (!gameStates.containsKey(token.getGameId())) {
            val createdGame = getCreatedGameOrThrow(token)
            gameStates[token.getGameId()] = GameStateLogic(createdGame.getCreator(), drawerFactory)
        }
        return gameStates[token.getGameId()]!!
    }
}
