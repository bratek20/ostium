package com.github.bratek20.ostium.kajiugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.carddrawer.context.CardDrawerMocks
import com.github.bratek20.ostium.carddrawer.fixtures.CardDrawerApiMock
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameToken
import com.github.bratek20.ostium.kajiugame.api.GameApi
import com.github.bratek20.ostium.kajiugame.api.GameNotFoundException
import com.github.bratek20.ostium.kajiugame.api.TurnPhase
import com.github.bratek20.ostium.kajiugame.context.KajiuGameImpl
import com.github.bratek20.ostium.kajiugame.fixtures.ExpectedHitZone
import com.github.bratek20.ostium.kajiugame.fixtures.assertGameState
import com.github.bratek20.ostium.user.fixtures.username
import org.junit.jupiter.api.BeforeEach
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

    @Test
    fun `should return default state`() {
        //given
        val token = gamesManagementApi.create(username())
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
        //when
        val state = api.getState(token)

        //then
        val expectedEmptyZone: (ExpectedHitZone.() -> Unit) = {
            leftReceiver = {
                type =  ExpectedDamageType.Light
                myDamage = 0
                opponentDamage = 0
            }
            centerReceiver = {
                type =  ExpectedDamageType.Medium
                myDamage = 0
                opponentDamage = 0
            }
            rightReceiver = {
                type =  ExpectedDamageType.Heavy
                myDamage = 0
                opponentDamage = 0
            }
        }
        assertGameState(state) {
            turn = 1
            phase = ExpectedTurnPhase.PlayCard
            myReady = false
            opponentReady = false
            table = {
                leftZone = expectedEmptyZone
                centerZone = expectedEmptyZone
                rightZone = expectedEmptyZone
            }
            hand = {
                cards = listOf(
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
            }
        }
    }
}