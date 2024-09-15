package com.github.bratek20.ostium.singlegame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamesmanagement.api.GameId
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertGameId
import com.github.bratek20.ostium.singlegame.api.GameNotFoundException
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
import com.github.bratek20.ostium.singlegame.api.RowType
import com.github.bratek20.ostium.singlegame.context.SingleGameImpl
import com.github.bratek20.ostium.singlegame.fixtures.assertGameState
import com.github.bratek20.ostium.singlegame.fixtures.creatureCardId
import com.github.bratek20.ostium.user.api.Username
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
        val GAME_ID: GameId = GameId(1)
        val CREATOR: Username = Username("creator")
        val JOINER: Username = Username("joiner")
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
            assertGameId(gameId, 1)
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

                val state1 = api.playCard(GAME_ID, CREATOR, creatureCardId("Mouse1"), RowType.ATTACK)
                val state2 = api.playCard(GAME_ID, JOINER, creatureCardId("Mouse2"), RowType.DEFENSE)

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
        }
    }


//

//
//    @Test
//    fun `move card to empty row`() {
//        api.startGame()
//        api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)
//        loggerMock.reset()
//
//        val game = api.moveCard(creatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE)
//
//        assertGame(game) {
//            hand = {
//                cards = listOf {
//                    id = "Mouse2"
//                }
//            }
//            table = {
//                attackRow = {
//                    cardEmpty = true
//                }
//                defenseRow = {
//                    card = {
//                        id = "Mouse1"
//                    }
//                }
//            }
//        }
//        loggerMock.assertInfos(
//            "Card Mouse1 moved from ATTACK to DEFENSE row"
//        )
//    }
//
//    @Test
//    fun `move card to row with card`() {
//        api.startGame()
//        api.playCard(creatureCardId("Mouse1"), RowType.ATTACK)
//        api.playCard(creatureCardId("Mouse2"), RowType.DEFENSE)
//
//        val game = api.moveCard(creatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE)
//
//        assertGame(game) {
//            table = {
//                attackRow = {
//                    card = {
//                        id = "Mouse2"
//                    }
//                }
//                defenseRow = {
//                    card = {
//                        id = "Mouse1"
//                    }
//                }
//            }
//        }
//    }

}