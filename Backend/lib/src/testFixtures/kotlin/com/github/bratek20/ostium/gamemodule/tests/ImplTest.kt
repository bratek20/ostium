package com.github.bratek20.ostium.gamemodule.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamemodule.api.GameApi
import com.github.bratek20.ostium.gamemodule.api.RowType
import com.github.bratek20.ostium.gamemodule.context.GameModuleImpl
import com.github.bratek20.ostium.gamemodule.fixtures.assertGame
import com.github.bratek20.ostium.gamemodule.fixtures.creatureCardId
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

open class GameModuleImplTest {
    class Context(
        val api: GameApi,
        val loggerMock: LoggerMock
    )
    open fun createContext(): Context {
        val c = someContextBuilder()
            .withModules(
                GameModuleImpl(),
                LogsMocks()
            ).build()

        return Context(
            api = c.get(GameApi::class.java),
            loggerMock = c.get(LoggerMock::class.java)
        )
    }

    private lateinit var api: GameApi
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
                    cardEmpty = true
                }
                defenseRow = {
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
    }

    @Test
    fun `move card`() {
        api.startGame()
        api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)

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
    }
}