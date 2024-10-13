package com.github.bratek20.ostium.kaijugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.carddrawing.context.CardDrawerMocks
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

        api = c.get(GameApi::class.java)
        scenarios = c.get(KaijuGameScenarios::class.java)
    }

    @Nested
    inner class NoGameScope {
        @Test
        fun `should throw api exception if game not found`() {
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
                drawerMockCards = listOf(
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
    inner class PlayCardScope {
        @Test
        fun `should play card - opponent does not see`() {
            val si = scenarios.inPhase {
                phase = TurnPhase.PlayCard
                drawerMockCards = listOf (
                    {
                        type = "Light"
                    },
                    {
                        type = "Medium"
                        value = 2
                        focusCost = 1
                    },
                )
            }
            val expectedCard0: ExpectedCard.() -> Unit = {
                type = "Light"
            }
            val expectedCard1: ExpectedCard.() -> Unit = {
                type = "Medium"
                value = 2
                focusCost = 1
            }

            api.playCard(si.creatorToken, 1).let {
                assertGameState(it) {
                    hand = {
                        cards = listOf(
                            expectedCard0,
                            expectedCard0,
                            expectedCard1
                        )
                    }
                    table = {
                        mySide = {
                            pool = {
                                mediumGiver = {
                                    damageValue = 2
                                }
                                focusLeft = 1
                            }
                            playedCards = listOf(
                                expectedCard1
                            )
                        }
                    }
                }
            }

            api.getState(si.joinerToken).let {
                assertGameState(it) {
                    table = {
                        opponentSide = {
                            playedCards = emptyList()
                        }
                    }
                }
            }
        }
    }

    @Nested
    inner class AssignDamageScope {
        private lateinit var si: KaijuGameScenarios.InGameStateInfo

        @BeforeEach
        fun `creator has 2 medium damage in pool`() {
            si = scenarios.inGame {
                drawerMockCards = listOf {
                    type = "Medium"
                    value = 2
                    focusCost = 2
                }
            }

            api.playCard(si.creatorToken, 0)

            scenarios.progressToPhase(si, TurnPhase.AssignDamage)


            api.getState(si.creatorToken).let {
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
            api.assignDamage(si.creatorToken, HitZonePosition.Center, DamageType.Medium).let {
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

            api.getState(si.joinerToken).let {
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

    @Nested
    inner class AssignGuardScope {
        @Test
        fun `should assign unspect focus as guard - redues opponent damage`() {
            val si = scenarios.inPhase {
                phase = TurnPhase.AssignGuard
            }
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
        fun `should end PlayCard phase - opponent sees that`() {
            val si = scenarios.inGame()

            api.endPhase(si.creatorToken).let {
                assertGameState(it) {
                    myReady = true
                }
            }

            api.getState(si.joinerToken).let {
                assertGameState(it) {
                    opponentReady = true
                }
            }
        }

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
        private lateinit var si: KaijuGameScenarios.InGameStateInfo

        @BeforeEach
        fun `some cards played, damage and guard assigned - by both players`() {
            si = scenarios.inGame {
                drawerMockCards = listOf (
                    {
                        type = "Light"
                        value = 5
                        focusCost = 0
                    },
                    {
                        type = "Medium"
                        value = 5
                        focusCost = 1
                    }
                )
            }

            api.playCard(si.creatorToken, 0) // Light
            api.playCard(si.joinerToken, 1) // Medium

            scenarios.progressToPhase(si, TurnPhase.AssignDamage)

            api.assignDamage(si.creatorToken, HitZonePosition.Center, DamageType.Light) // 5 dmg
            api.assignDamage(si.joinerToken, HitZonePosition.Center, DamageType.Medium) // 5 dmg

            scenarios.progressToPhase(si, TurnPhase.AssignGuard)

            api.assignGuard(si.creatorToken, HitZonePosition.Center, DamageType.Medium) // -2 dmg
            api.assignGuard(si.joinerToken, HitZonePosition.Center, DamageType.Light) // -1 dmg
        }

        @Test
        fun `should not show opponent state before reveal phase`() {
            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    table = {
                        centerZone = {
                            lightReceiver = {
                                myDamage = 5
                                opponentDamage = 0
                            }
                            mediumReceiver = {
                                myDamage = 0
                                opponentDamage = -2
                            }
                        }
                        mySide = {
                            playedCards = listOf {
                                type = "Light"
                            }
                        }
                        opponentSide = {
                            playedCards = emptyList()
                        }
                    }
                }
            }

            api.getState(si.joinerToken).let {
                assertGameState(it) {
                    table = {
                        centerZone = {
                            lightReceiver = {
                                myDamage = 0
                                opponentDamage = -1
                            }
                            mediumReceiver = {
                                myDamage = 5
                                opponentDamage = 0
                            }
                        }
                        mySide = {
                            playedCards = listOf {
                                type = "Medium"
                            }
                        }
                        opponentSide = {
                            playedCards = emptyList()
                        }
                    }
                }
            }
        }

        @Test
        fun `should show opponent state in reveal phase`() {
            api.endPhase(si.creatorToken)
            api.endPhase(si.joinerToken)

            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    phase = ExpectedTurnPhase.Reveal
                    table = {
                        centerZone = {
                            lightReceiver = {
                                myDamage = 4
                                opponentDamage = 0
                            }
                            mediumReceiver = {
                                myDamage = 0
                                opponentDamage = 3
                            }
                        }
                        mySide = {
                            playedCards = listOf {
                                type = "Light"
                            }
                        }
                        opponentSide = {
                            playedCards = listOf {
                                type = "Medium"
                            }
                        }
                    }
                }
            }

            api.getState(si.joinerToken).let {
                assertGameState(it) {
                    phase = ExpectedTurnPhase.Reveal
                    table = {
                        centerZone = {
                            lightReceiver = {
                                myDamage = 0
                                opponentDamage = 4
                            }
                            mediumReceiver = {
                                myDamage = 3
                                opponentDamage = 0
                            }
                        }
                        mySide = {
                            playedCards = listOf {
                                type = "Medium"
                            }
                        }
                        opponentSide = {
                            playedCards = listOf {
                                type = "Light"
                            }
                        }
                    }
                }
            }
        }
    }

    @Nested
    inner class NextTurnScope {
        @Test
        fun `should have 4 focus and draw to 4 cards in second turn`() {
            val si = scenarios.inPhase {
                phase = TurnPhase.PlayCard
                drawerMockCards = listOf(
                    {
                        type = "Light"
                        focusCost = 1
                    },
                    {
                        type = "Medium"
                    },
                )
            }

            api.playCard(si.creatorToken, 2)
            api.playCard(si.creatorToken, 0)

            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    hand = {
                        cards = listOf(
                            {
                                type = "Medium"
                            },
                            {
                                type = "Medium"
                            },
                        )
                    }
                }
            }

            scenarios.progressToPhase(si, TurnPhase.Reveal)

            //when
            api.endPhase(si.creatorToken)
            api.endPhase(si.joinerToken)

            //then
            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    turn = 2
                    phase = ExpectedTurnPhase.PlayCard
                    table = {
                        mySide = {
                            pool = {
                                focusLeft = 4
                            }
                        }
                    }
                    hand = {
                        cards = listOf(
                            {
                                type = "Medium"
                            },
                            {
                                type = "Medium"
                            },
                            {
                                type = "Light"
                            },
                            {
                                type = "Medium"
                            },
                        )
                    }
                }
            }
        }
    }

    @Nested
    inner class ManyTurnsScope {
        @Test
        fun `should never end game, focus increases by 2 every turn`() {
            val si = scenarios.inGame()

            scenarios.progressToTurn(si, 100)

            api.getState(si.creatorToken).let {
                assertGameState(it) {
                    turn = 100
                    table = {
                        mySide = {
                            pool = {
                                focusLeft = 200
                            }
                        }
                    }
                }
            }
        }
    }
}