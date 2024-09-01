package com.github.bratek20.ostium.gamesetup.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamecomponents.fixtures.creatureCardId
import com.github.bratek20.ostium.gamesetup.api.GameSetupApi
import com.github.bratek20.ostium.gamesetup.api.RowType
import com.github.bratek20.ostium.gamesetup.context.GameSetupImpl
import com.github.bratek20.ostium.gamesetup.fixtures.assertGame
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

open class GameSetupImplTest {
    class Context(
        val api: GameSetupApi,
        val loggerMock: LoggerMock
    )
    open fun createContext(): Context {
        val c = someContextBuilder()
            .withModules(
                GameSetupImpl(),
                LogsMocks()
            ).build()

        return Context(
            api = c.get(GameSetupApi::class.java),
            loggerMock = c.get(LoggerMock::class.java)
        )
    }

    private lateinit var api: GameSetupApi
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
                attackRowEmpty = true
                defenseRowEmpty = true
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
                    id = "Mouse1"
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
                attackRowEmpty = true
                defenseRow = {
                    id = "Mouse1"
                }
            }
        }
    }
}