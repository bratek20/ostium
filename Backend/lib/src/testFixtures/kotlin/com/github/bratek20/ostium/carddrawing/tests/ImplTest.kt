package com.github.bratek20.ostium.carddrawing.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.carddrawing.api.CardDrawer
import com.github.bratek20.ostium.carddrawing.api.CardDrawerFactory
import com.github.bratek20.ostium.carddrawing.context.CardDrawingImpl
import com.github.bratek20.ostium.kaijugame.api.Card
import com.github.bratek20.ostium.kaijugame.api.DamageType
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

class CardDrawingImplTest {
    private lateinit var drawnCards: List<Card>

    @BeforeEach
    fun `draw 100 cards`() {
        val cardDrawer = someContextBuilder()
            .withModules(CardDrawingImpl())
            .get(CardDrawerFactory::class.java)
            .create()

        drawnCards = (1..100).map { cardDrawer.draw() }
    }

    @Test
    fun `should draw cards of all types, focus cost from 1 to 5 and value from 1 to 5`() {
        val cardTypes = drawnCards.map { it.getType() }.toSet()
        assertThat(cardTypes).containsExactlyInAnyOrder(
            DamageType.Light,
            DamageType.Medium,
            DamageType.Heavy,
        )

        val cardFocusCosts = drawnCards.map { it.getFocusCost() }.toSet()
        assertThat(cardFocusCosts).containsExactlyInAnyOrder(1, 2, 3, 4, 5)

        val cardValues = drawnCards.map { it.getValue() }.toSet()
        assertThat(cardValues).containsExactlyInAnyOrder(1, 2, 3, 4, 5)
    }
}