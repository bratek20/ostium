package com.github.bratek20.ostium.kajiugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.carddrawer.context.CardDrawerMocks
import com.github.bratek20.ostium.carddrawer.fixtures.CardDrawerApiMock
import com.github.bratek20.ostium.gamesmanagement.api.GameToken
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameToken
import com.github.bratek20.ostium.kajiugame.api.GameApi
import com.github.bratek20.ostium.kajiugame.api.GameNotFoundException
import com.github.bratek20.ostium.kajiugame.context.KajiuGameImpl
import com.github.bratek20.ostium.kajiugame.fixtures.ExpectedCard
import com.github.bratek20.ostium.kajiugame.fixtures.ExpectedHitZone
import com.github.bratek20.ostium.kajiugame.fixtures.ExpectedPlayerSide
import com.github.bratek20.ostium.kajiugame.fixtures.assertGameState
import com.github.bratek20.ostium.user.fixtures.username
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test

class ExpectedTurnPhase {
    companion object {
        val PlayCard = "PlayCard"
    }
}

class ExpectedDamageType {
    companion object {
        val Light = "Light"
        val Medium = "Medium"
        val Heavy = "Heavy"
    }
}

class KajiuGameImplTest {

    private lateinit var api: GameApi
    private lateinit var gamesManagementApi: GamesManagementApi
    private lateinit var cardDrawerApiMock: CardDrawerApiMock

    @BeforeEach
    fun createContext() {
        val c = someContextBuilder()
            .withModules(
                LogsMocks(),
                GamesManagementImpl(),

                CardDrawerMocks(),

                KajiuGameImpl()
            )
            .build()

        gamesManagementApi = c.get(GamesManagementApi::class.java)
        api = c.get(GameApi::class.java)
        cardDrawerApiMock = c.get(CardDrawerApiMock::class.java)
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
    inner class GameCreated {
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

        private lateinit var token: GameToken

        @BeforeEach
        fun `game created and cards to be drawn set`() {
            token = gamesManagementApi.create(username())
            cardDrawerApiMock.setCards(
                listOf(
                    {
                        type = "Light"
                        value = 1
                        focusCost = 1
                    },
                    {
                        type = "Medium"
                        value = 2
                        focusCost = 2
                    },
                    {
                        type = "Heavy"
                        value = 3
                        focusCost = 3
                    },
                    {
                        type = "Heavy"
                        value = 4
                        focusCost = 4
                    },
                )
            )
        }

        @Test
        fun `should return initial state`() {
            //when
            val state = api.getState(token)

            //then
            assertGameState(state) {
                turn = 1
                phase = ExpectedTurnPhase.PlayCard
                myReady = false
                opponentReady = false
                table = {
                    leftZone = expectedInitialHitZone()
                    centerZone = expectedInitialHitZone()
                    rightZone = expectedInitialHitZone()
                    mySide = expectedInitialPlayerSide()
                    opponentSide = expectedInitialPlayerSide()
                }
                hand = {
                    cards = listOf(
                        expectedCard0,
                        expectedCard1,
                        expectedCard2,
                        expectedCard3
                    )
                }
            }
        }

        @Test
        fun `should play card`() {
            api.playCard(token, 1).let {
                assertGameState(it) {
                    hand = {
                        cards = listOf(
                            expectedCard0,
                            expectedCard2,
                            expectedCard3,
                        )
                    }
                }
            }
        }
    }

    private fun expectedInitialHitZone(): (ExpectedHitZone.() -> Unit) = {
        leftReceiver = {
            type = ExpectedDamageType.Light
            myDamage = 0
            opponentDamage = 0
        }
        centerReceiver = {
            type = ExpectedDamageType.Medium
            myDamage = 0
            opponentDamage = 0
        }
        rightReceiver = {
            type = ExpectedDamageType.Heavy
            myDamage = 0
            opponentDamage = 0
        }
    }

    private fun expectedInitialPlayerSide(): (ExpectedPlayerSide.() -> Unit) = {
        pool = {
            attackGivers = listOf(
                {
                    type = ExpectedDamageType.Light
                    damageValue = 0
                },
                {
                    type = ExpectedDamageType.Medium
                    damageValue = 0
                },
                {
                    type = ExpectedDamageType.Heavy
                    damageValue = 0
                },
            )
            focusLeft = 2
        }
        playedCards = emptyList()
    }
}