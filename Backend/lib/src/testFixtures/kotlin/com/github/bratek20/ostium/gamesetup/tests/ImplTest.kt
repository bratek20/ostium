package com.github.bratek20.ostium.gamesetup.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.gamecomponents.fixtures.creatureCardId
import com.github.bratek20.ostium.gamesetup.api.GameSetupApi
import com.github.bratek20.ostium.gamesetup.api.RowType
import com.github.bratek20.ostium.gamesetup.context.GameSetupImpl
import com.github.bratek20.ostium.gamesetup.fixtures.assertGame
import org.junit.jupiter.api.Test

open class GameSetupImplTest {
    open fun createApi(): GameSetupApi {
        return someContextBuilder()
            .withModules(
                GameSetupImpl()
            )
            .get(GameSetupApi::class.java)
    }

    @Test
    fun `start game`() {
        val api = createApi()

        val game = api.startGame()

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
        val api = createApi()
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
        val api = createApi()
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