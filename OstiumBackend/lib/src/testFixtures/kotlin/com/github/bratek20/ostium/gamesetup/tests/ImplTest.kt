package com.github.bratek20.ostium.gamesetup.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.gamesetup.api.GameSetupApi
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
                        name = "HandCard1"
                    },
                    {
                        name = "HandCard2"
                    }
                )
            }
            table = {
                creatureCard = {
                    name = "TableCreature"
                }
                gateCard = {
                    destroyed = false
                }
                gateDurabilityCard = {
                    myMarker = 15
                    opponentMarker = 15
                }
            }
        }
    }
}