package com.github.bratek20.ostium.kaijugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.carddrawing.context.CardDrawerMocks
import com.github.bratek20.ostium.carddrawing.fixtures.CardDrawerApiMock
import com.github.bratek20.ostium.gamesmanagement.api.GameToken
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameToken
import com.github.bratek20.ostium.kaijugame.api.*
import com.github.bratek20.ostium.kaijugame.context.KaijuGameImpl
import com.github.bratek20.ostium.kaijugame.fixtures.*
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test

class ExpectedTurnPhase {
    companion object {
        val PlayCard = "PlayCard"
        val AssignDamage = "AssignDamage"
        val AssignGuard = "AssignGuard"
        val Reveal = "Reveal"
    }
}

class ExpectedDamageType {
    companion object {
        val Light = "Light"
        val Medium = "Medium"
        val Heavy = "Heavy"
    }
}

class KaijuGameImplTest {

    private lateinit var api: GameApi
    private lateinit var gamesManagementApi: GamesManagementApi
    private lateinit var cardDrawerApiMock: CardDrawerApiMock
    private lateinit var scenarios: KaijuGameScenarios

    @BeforeEach
    fun createContext() {
        val c = someContextBuilder()
            .withModules(
                LogsMocks(),
                GamesManagementImpl(),

                CardDrawerMocks(),

                KaijuGameImpl(),

                KaijuGameScenariosModule()
            )
            .build()

        gamesManagementApi = c.get(GamesManagementApi::class.java)
        api = c.get(GameApi::class.java)
        cardDrawerApiMock = c.get(CardDrawerApiMock::class.java)
        scenarios = c.get(KaijuGameScenarios::class.java)
    }

    @Test
    fun `should throw api exception if game not created`() {
        assertApiExceptionThrown(
            {
                api.getState(gameToken {
                    gameId = 42
                })
            },
            {
                type = GameNotFoundException::class
                message = "Game for id 42 not found"
            }
        )
    }

    @Nested
    inner class InitialStateScope {
        val card0Def: CardDef.() -> Unit = {
            type = "Light"
            value = 1
            focusCost = 1
        }
        val expectedCard0: ExpectedCard.() -> Unit = {
            type = "Light"
            value = 1
            focusCost = 1
        }

        val card1Def: CardDef.() -> Unit = {
            type = "Medium"
            value = 2
            focusCost = 2
        }
        val expectedCard1: ExpectedCard.() -> Unit = {
            type = "Medium"
            value = 2
            focusCost = 2
        }

        val card2Def: CardDef.() -> Unit = {
            type = "Heavy"
            value = 3
            focusCost = 3
        }
        val expectedCard2: ExpectedCard.() -> Unit = {
            type = "Heavy"
            value = 3
            focusCost = 3
        }

        @Test
        fun `should return same initial state for both players`() {
            val si = scenarios.inGame {
                cards = listOf(
                    card0Def,
                    card1Def,
                    card2Def
                )
            }

            val expectedInitialHitZone: (expectedPosition: String) -> (ExpectedHitZone.() -> Unit) = { expectedPosition ->
                {
                    position = expectedPosition
                    lightReceiver = {
                        type = ExpectedDamageType.Light
                        myDamage = 0
                        opponentDamage = 0
                    }
                    mediumReceiver = {
                        type = ExpectedDamageType.Medium
                        myDamage = 0
                        opponentDamage = 0
                    }
                    heavyReceiver = {
                        type = ExpectedDamageType.Heavy
                        myDamage = 0
                        opponentDamage = 0
                    }
                }
            }

            val expectedInitialPlayerSide: (ExpectedPlayerSide.() -> Unit) = {
                pool = {
                    lightGiver = {
                        type = ExpectedDamageType.Light
                        damageValue = 0
                    }
                    mediumGiver = {
                        type = ExpectedDamageType.Medium
                        damageValue = 0
                    }
                    heavyGiver = {
                        type = ExpectedDamageType.Heavy
                        damageValue = 0
                    }
                    focusLeft = 2
                }
                playedCards = emptyList()
            }

            val expectedInitialState: ExpectedGameState.() -> Unit = {
                turn = 1
                phase = ExpectedTurnPhase.PlayCard
                myReady = false
                opponentReady = false
                table = {
                    leftZone = expectedInitialHitZone("Left")
                    centerZone = expectedInitialHitZone("Center")
                    rightZone = expectedInitialHitZone("Right")
                    mySide = expectedInitialPlayerSide
                    opponentSide = expectedInitialPlayerSide
                }
                hand = {
                    cards = listOf(
                        expectedCard0,
                        expectedCard1,
                        expectedCard2,
                        expectedCard0,
                    )
                }
            }

            //when
            val creatorState = api.getState(si.creatorToken)
            val joinerState = api.getState(si.joinerToken)

            //then
            assertGameState(creatorState, expectedInitialState)
            assertGameState(joinerState, expectedInitialState)
        }
    }

    @Nested
    inner class ToRefactorScope {
        val card0Def: CardDef.() -> Unit = {
            type = "Light"
            value = 1
            focusCost = 1
        }
        val card1Def: CardDef.() -> Unit = {
            type = "Medium"
            value = 2
            focusCost = 2
        }
        val card2Def: CardDef.() -> Unit = {
            type = "Heavy"
            value = 3
            focusCost = 3
        }
        val card3Def: CardDef.() -> Unit = {
            type = "Heavy"
            value = 4
            focusCost = 4
        }

        val expectedCard0: ExpectedCard.() -> Unit = {
            type = "Light"
            value = 1
            focusCost = 1
        }
        val expectedCard1: ExpectedCard.() -> Unit = {
            type = "Medium"
            value = 2
            focusCost = 2
        }
        val expectedCard2: ExpectedCard.() -> Unit = {
            type = "Heavy"
            value = 3
            focusCost = 3
        }
        val expectedCard3: ExpectedCard.() -> Unit = {
            type = "Heavy"
            value = 4
            focusCost = 4
        }

        private lateinit var creatorToken: GameToken
        private lateinit var joinerToken: GameToken

        @BeforeEach
        fun `game created and cards to be drawn set`() {
            val si = scenarios.inGame()
            creatorToken = si.creatorToken
            joinerToken = si.joinerToken

            cardDrawerApiMock.setCards(
                listOf(
                    card0Def,
                    card1Def,
                    card2Def,
                    card3Def
                )
            )
        }

        @Test
        fun `should end PlayCard phase - opponent sees that`() {
            api.endPhase(creatorToken).let {
                assertGameState(it) {
                    myReady = true
                }
            }

            api.getState(joinerToken).let {
                assertGameState(it) {
                    opponentReady = true
                }
            }
        }

        @Test
        fun `should play card - opponent does not see`() {
            // assert just to show what card we are going to play
            assertCard(card(card1Def)) {
                type = ExpectedDamageType.Medium
                value = 2
                focusCost = 2
            }

            api.playCard(creatorToken, 1).let {
                assertGameState(it) {
                    hand = {
                        cards = listOf(
                            expectedCard0,
                            expectedCard2,
                            expectedCard3,
                        )
                    }
                    table = {
                        mySide = {
                            pool = {
                                mediumGiver = {
                                    damageValue = 2
                                }
                                focusLeft = 0
                            }
                            playedCards = listOf(
                                expectedCard1
                            )
                        }
                    }
                }
            }

            api.getState(joinerToken).let {
                assertGameState(it) {
                    table = {
                        opponentSide = {
                            playedCards = emptyList()
                        }
                    }
                }
            }
        }

        @Nested
        inner class InAssignDamagePhase {
            @BeforeEach
            fun `creator has 2 medium damage in pool`() {
                api.playCard(creatorToken, 1)
                api.endPhase(creatorToken)
                api.endPhase(joinerToken)

                api.getState(creatorToken).let {
                    assertGameState(it) {
                        phase = ExpectedTurnPhase.AssignDamage
                        table = {
                            mySide = {
                                pool = {
                                    mediumGiver = {
                                        damageValue = 2
                                    }
                                    focusLeft = 0
                                }
                            }
                        }
                    }
                }
            }

            @Test
            fun `should assign damage from pool - opponent does not see`() {
                api.assignDamage(creatorToken, HitZonePosition.Center, DamageType.Medium).let {
                    assertGameState(it) {
                        table = {
                            centerZone = {
                                mediumReceiver = {
                                    myDamage = 2
                                }
                            }
                            mySide = {
                                pool = {
                                    mediumGiver = {
                                        damageValue = 0
                                    }
                                }
                            }
                        }
                    }
                }

                api.getState(joinerToken).let {
                    assertGameState(it) {
                        table = {
                            centerZone = {
                                mediumReceiver = {
                                    myDamage = 0
                                    opponentDamage = 0
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    @Nested
    inner class AssignGuardScope {
        @Test
        fun `should assign unspect focus as guard - redues opponent damage`() {
            val si = scenarios.inPhase(TurnPhase.AssignGuard)
            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    table = {
                        centerZone = {
                            heavyReceiver = {
                                opponentDamage = 0
                            }
                        }
                        mySide = {
                            pool = {
                                focusLeft = 2
                            }
                        }
                    }
                }
            }

            //when & then
            api.assignGuard(si.creatorToken, HitZonePosition.Center, DamageType.Heavy).let {
                assertGameState(it) {
                    table = {
                        centerZone = {
                            heavyReceiver = {
                                opponentDamage = -2
                            }
                        }
                        mySide = {
                            pool = {
                                focusLeft = 0
                            }
                        }
                    }
                }
            }
        }
    }

    @Nested
    inner class ChangingPhasesScope {
        @Test
        fun `should change phases`() {
            val si = scenarios.inGame()

            val endPhaseByBothPlayers = {
                api.endPhase(si.creatorToken)
                api.endPhase(si.joinerToken)
            }

            val assertPhase = { expectedPhase: String ->
                api.getState(si.creatorToken).let {
                    assertGameState(it) {
                        phase = expectedPhase
                    }
                }
            }

            assertPhase(ExpectedTurnPhase.PlayCard)
            endPhaseByBothPlayers()

            assertPhase(ExpectedTurnPhase.AssignDamage)
            endPhaseByBothPlayers()

            assertPhase(ExpectedTurnPhase.AssignGuard)
            endPhaseByBothPlayers()

            assertPhase(ExpectedTurnPhase.Reveal)
            endPhaseByBothPlayers()

            assertPhase(ExpectedTurnPhase.PlayCard)
        }
    }

    @Nested
    inner class RevealScope {
        @Test
        fun `should not show opponent state before reveal phase`() {

        }

        @Test
        fun `should show opponent in reveal phase`() {

        }
    }

}