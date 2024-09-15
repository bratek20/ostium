package com.github.bratek20.ostium.singlegame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamesmanagement.api.GameId
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertGameId
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameId
import com.github.bratek20.ostium.singlegame.api.GameNotFoundException
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
import com.github.bratek20.ostium.singlegame.api.RowType
import com.github.bratek20.ostium.singlegame.context.SingleGameImpl
import com.github.bratek20.ostium.singlegame.fixtures.assertGameState
import com.github.bratek20.ostium.singlegame.fixtures.creatureCardId
import com.github.bratek20.ostium.user.api.Username
import com.github.bratek20.ostium.user.fixtures.username
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test

open class SingleGameImplTest {
    class Context(
        val managementApi: GamesManagementApi,
        val api: SingleGameApi,
        val loggerMock: LoggerMock
    )
    open fun createContext(): Context {
        val c = someContextBuilder()
            .withModules(
                GamesManagementImpl(),
                SingleGameImpl(),
                LogsMocks()
            ).build()

        return Context(
            managementApi = c.get(GamesManagementApi::class.java),
            api = c.get(SingleGameApi::class.java),
            loggerMock = c.get(LoggerMock::class.java)
        )
    }

    private lateinit var managementApi: GamesManagementApi
    private lateinit var api: SingleGameApi
    private lateinit var loggerMock: LoggerMock

    companion object {
        val GAME_ID = gameId(1)
        val CREATOR = username("creator")
        val JOINER = username("joiner")
        
        val MOUSE_1 = creatureCardId("Mouse1")
        val MOUSE_2 = creatureCardId("Mouse2")
    }

    @BeforeEach
    fun setup() {
        val c = createContext()
        this.managementApi = c.managementApi
        this.api = c.api
        this.loggerMock = c.loggerMock
    }

    @Test
    fun `should throw if game not found`() {

        assertApiExceptionThrown(
            { api.getState(GAME_ID, CREATOR) },
            {
                type = GameNotFoundException::class
                message = "Game 1 for user `creator` not found"
            }
        )

    }

    @Nested
    inner class OneGameScope {
        @BeforeEach
        fun singleGameCreated() {
            val gameId = managementApi.create(CREATOR)
            assertGameId(gameId, GAME_ID.value)
        }

        @Test
        fun `initial creator game state`() {
            val game = api.getState(GAME_ID, CREATOR)

            assertGameState(game) {
                myName = CREATOR.value
                opponentName = null
                myHand = {
                    cards = listOf(
                        {
                            id = "Mouse1"
                        },
                        {
                            id = "Mouse2"
                        }
                    )
                }
                opponentHand = {
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
                    mySide = {
                        attackRow = {
                            type = "ATTACK"
                            cardEmpty = true
                        }
                        defenseRow = {
                            type = "DEFENSE"
                            cardEmpty = true
                        }
                    }
                    opponentSide = {
                        attackRow = {
                            type = "ATTACK"
                            cardEmpty = true
                        }
                        defenseRow = {
                            type = "DEFENSE"
                            cardEmpty = true
                        }
                    }
                }
            }
        }

        @Test
        fun `other created game should heve separate state`() {
            api.playCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK)

            val newGameId = managementApi.create(JOINER)

            val state = api.getState(newGameId, JOINER)

            assertGameState(state) {
                myName = JOINER.value
                opponentName = null
                table = {
                    mySide = {
                        attackRow = {
                            cardEmpty = true
                        }
                    }
                    opponentSide = {
                        attackRow = {
                            cardEmpty = true
                        }
                    }
                }
            }
        }

        @Nested
        inner class OpponentJoinedScope {
            @BeforeEach
            fun opponentJoined() {
                managementApi.join(JOINER, GAME_ID)
            }

            @Test
            fun `initial opponent game state`() {
                val game = api.getState(GAME_ID, JOINER)

                assertGameState(game) {
                    myName = JOINER.value
                    opponentName = CREATOR.value
                }
            }

            @Test
            fun `creator and joiner played card`() {
                loggerMock.reset()

                val state1 = api.playCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK)
                val state2 = api.playCard(GAME_ID, JOINER, MOUSE_2, RowType.DEFENSE)

                assertGameState(state1) {
                    myHand = {
                        cards = listOf {
                            id = "Mouse2"
                        }
                    }
                    opponentHand = {
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
                        mySide = {
                            attackRow = {
                                card = {
                                    id = "Mouse1"
                                }
                            }
                            defenseRow = {
                                cardEmpty = true
                            }
                        }
                        opponentSide = {
                            attackRow = {
                                cardEmpty = true
                            }
                            defenseRow = {
                                cardEmpty = true
                            }
                        }
                    }
                }
                assertGameState(state2) {
                    myHand = {
                        cards = listOf {
                            id = "Mouse1"
                        }
                    }
                    opponentHand = {
                        cards = listOf {
                            id = "Mouse2"
                        }
                    }
                    table = {
                        mySide = {
                            attackRow = {
                                cardEmpty = true
                            }
                            defenseRow = {
                                card = {
                                    id = "Mouse2"
                                }
                            }
                        }
                        opponentSide = {
                            attackRow = {
                                card = {
                                    id = "Mouse1"
                                }
                            }
                            defenseRow = {
                                cardEmpty = true
                            }
                        }
                    }
                }

                loggerMock.assertInfos(
                    "User `creator` played card `Mouse1` in ATTACK row",
                    "User `joiner` played card `Mouse2` in DEFENSE row"
                )
            }

            @Test
            fun `creator and joiner moved card to empty row`() {
                api.playCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK)
                api.playCard(GAME_ID, JOINER, MOUSE_2, RowType.DEFENSE)

                loggerMock.reset()

                val state1 = api.moveCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK, RowType.DEFENSE)
                val state2 = api.moveCard(GAME_ID, JOINER, MOUSE_2, RowType.DEFENSE, RowType.ATTACK)

                assertGameState(state1) {
                    myHand = {
                        cards = listOf {
                            id = "Mouse2"
                        }
                    }
                    table = {
                        mySide = {
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
                assertGameState(state2) {
                    myHand = {
                        cards = listOf {
                            id = "Mouse1"
                        }
                    }
                    table = {
                        mySide = {
                            attackRow = {
                                card = {
                                    id = "Mouse2"
                                }
                            }
                            defenseRow = {
                                cardEmpty = true
                            }
                        }
                    }
                }
                loggerMock.assertInfos(
                    "User `creator` moved card `Mouse1` from ATTACK to DEFENSE row",
                    "User `joiner` moved card `Mouse2` from DEFENSE to ATTACK row"
                )
            }

            @Test
            fun `creator moved card to row with card`() {
                api.playCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK)
                api.playCard(GAME_ID, CREATOR, MOUSE_2, RowType.DEFENSE)
        
                val state = api.moveCard(GAME_ID, CREATOR, MOUSE_1, RowType.ATTACK, RowType.DEFENSE)

                assertGameState(state) {
                    table = {
                        mySide = {
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
        }
    }
}