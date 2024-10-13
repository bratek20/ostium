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

    class InGameArgs(
        var cards: List<CardDef.() -> Unit> = listOf({})
    )
    open class InGameStateInfo(
        val creatorToken: GameToken,
        val joinerToken: GameToken
    )
    fun inGame(argsInit: InGameArgs.() -> Unit = {}): InGameStateInfo {
        val args = InGameArgs().apply(argsInit)

        val creatorToken = gamesManagementApi.create(username("Player1"))
        val joinerToken = gamesManagementApi.join(username("Player2"), creatorToken.getGameId())

        cardDrawerFactoryMock.setCards(args.cards)

        return InGameStateInfo(creatorToken, joinerToken)
    }

    class InPhaseStateInfo(
        inGameSI: InGameStateInfo
    ): InGameStateInfo(inGameSI.joinerToken, inGameSI.creatorToken) {
    }

    fun inPhase(phase: TurnPhase): InPhaseStateInfo {
        val inGameSI = inGame()

        var state = api.getState(inGameSI.creatorToken)
        while (state.getPhase() != phase) {
            api.endPhase(inGameSI.creatorToken)
            api.endPhase(inGameSI.joinerToken)
            state = api.getState(inGameSI.creatorToken)
        }
        return InPhaseStateInfo(inGameSI)
    }
}

class KaijuGameScenariosModule: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setClass(KaijuGameScenarios::class.java)
    }

}