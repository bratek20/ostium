package com.github.bratek20.ostium.kaijugame.fixtures

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.ostium.carddrawing.fixtures.CardDrawerFactoryMock
import com.github.bratek20.ostium.gamesmanagement.api.GameToken
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.kaijugame.api.GameApi
import com.github.bratek20.ostium.kaijugame.api.TurnPhase
import com.github.bratek20.ostium.user.fixtures.username

class KaijuGameScenarios(
    private val api: GameApi,
    private val gamesManagementApi: GamesManagementApi,
    private val cardDrawerFactoryMock: CardDrawerFactoryMock
) {

    open class InGameArgs(
        var drawerMockCards: List<CardDef.() -> Unit> = listOf({})
    )
    open class InGameStateInfo(
        val creatorToken: GameToken,
        val joinerToken: GameToken
    )
    fun inGame(overriddenArgs: InGameArgs? = null, argsInit: InGameArgs.() -> Unit = {}): InGameStateInfo {
        val args = overriddenArgs ?: InGameArgs().apply(argsInit)

        val creatorToken = gamesManagementApi.create(username("Player1"))
        val joinerToken = gamesManagementApi.join(username("Player2"), creatorToken.getGameId())

        cardDrawerFactoryMock.setCards(args.drawerMockCards)

        return InGameStateInfo(creatorToken, joinerToken)
    }

    class InPhaseArgs(
        var phase: TurnPhase? = null
    ): InGameArgs()
    class InPhaseStateInfo(
        inGameSI: InGameStateInfo
    ): InGameStateInfo(inGameSI.joinerToken, inGameSI.creatorToken)
    fun inPhase(argsInit: InPhaseArgs.() -> Unit): InPhaseStateInfo {
        val args = InPhaseArgs().apply(argsInit)
        val inGameSI = inGame(overriddenArgs = args)

        progressToPhase(inGameSI, args.phase!!)

        return InPhaseStateInfo(inGameSI)
    }

    fun progressToPhase(si: InGameStateInfo, phase: TurnPhase) {
        var state = api.getState(si.creatorToken)
        while (state.getPhase() != phase) {
            api.endPhase(si.creatorToken)
            api.endPhase(si.joinerToken)
            state = api.getState(si.creatorToken)
        }
    }

    fun progressToTurn(si: InGameStateInfo, turn: Int) {
        var state = api.getState(si.creatorToken)
        while (state.getTurn() < turn) {
            api.endPhase(si.creatorToken)
            api.endPhase(si.joinerToken)
            state = api.getState(si.creatorToken)
        }
    }
}

class KaijuGameScenariosModule: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setClass(KaijuGameScenarios::class.java)
    }

}