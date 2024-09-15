package com.github.bratek20.ostium.singlegame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
import com.github.bratek20.ostium.singlegame.api.RowType
import com.github.bratek20.ostium.singlegame.context.SingleGameImpl
import com.github.bratek20.ostium.singlegame.fixtures.creatureCardId
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

open class SingleGameImplTest {
    class Context(
        val api: SingleGameApi,
        val loggerMock: LoggerMock
    )
    open fun createContext(): Context {
        val c = someContextBuilder()
            .withModules(
                SingleGameImpl(),
                LogsMocks()
            ).build()

        return Context(
            api = c.get(SingleGameApi::class.java),
            loggerMock = c.get(LoggerMock::class.java)
        )
    }

    private lateinit var api: SingleGameApi
    private lateinit var loggerMock: LoggerMock

    @BeforeEach
    fun setup() {
        val c = createContext()
        this.api = c.api
        this.loggerMock = c.loggerMock
    }

    @Test
    fun `start game`() {
        val game = api.startGame()

        loggerMock.assertInfos(
            "Game started"
        )
        assertGame(game) {
            hand = {
                cards = listOf(
                    {
                        id = "Mouse1"
                    },
                    {
                        id = "Mouse2"
                    }
                )
            }
            table = {
                gateDurabilityCard = {
                    myMarker = 15
                    opponentMarker = 15
                }
                attackRow = {
                    type = "ATTACK"
                    cardEmpty = true
                }
                defenseRow = {
                    type = "DEFENSE"
                    cardEmpty = true
                }
                gateCard = {
                    destroyed = false
                }
            }
        }
    }

    @Test
    fun `play card`() {
        api.startGame()
        loggerMock.reset()

        val game = api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)

        assertGame(game) {
            hand = {
                cards = listOf {
                    id = "Mouse2"
                }
            }
            table = {
                attackRow = {
                    card = {
                        id = "Mouse1"
                    }
                }
            }
        }
        loggerMock.assertInfos(
            "Card Mouse1 played in ATTACK row"
        )
    }

    @Test
    fun `move card to empty row`() {
        api.startGame()
        api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)
        loggerMock.reset()

        val game = api.moveCard(creatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE)

        assertGame(game) {
            hand = {
                cards = listOf {
                    id = "Mouse2"
                }
            }
            table = {
                attackRow = {
                    cardEmpty = true
                }
                defenseRow = {
                    card = {
                        id = "Mouse1"
                    }
                }
            }
        }
        loggerMock.assertInfos(
            "Card Mouse1 moved from ATTACK to DEFENSE row"
        )
    }

    @Test
    fun `move card to row with card`() {
        api.startGame()
        api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)
        api.playCard(creatureCardId("Mouse2"), RowType.DEFENSE)

        val game = api.moveCard(creatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE)

        assertGame(game) {
            table = {
                attackRow = {
                    card = {
                        id = "Mouse2"
                    }
                }
                defenseRow = {
                    card = {
                        id = "Mouse1"
                    }
                }
            }
        }
    }

}