package com.github.bratek20.ostium.carddrawing.fixtures

import com.github.bratek20.ostium.carddrawing.api.CardDrawer
import com.github.bratek20.ostium.carddrawing.api.CardDrawerFactory
import com.github.bratek20.ostium.kaijugame.api.Card
import com.github.bratek20.ostium.kaijugame.fixtures.CardDef
import com.github.bratek20.ostium.kaijugame.fixtures.card

class CardDrawerFactoryMock: CardDrawerFactory {
    private var cards: List<CardDef.() -> Unit> = listOf({})

    fun setCards(cards: List<CardDef.() -> Unit>) {
        this.cards = cards
    }

    override fun create(): CardDrawer {
        return CardDrawerMock(cards);
    }
}

class CardDrawerMock(
    private val cards: List<CardDef.() -> Unit>
): CardDrawer {
    private var index = 0
    override fun draw(): Card {
        val card = card(cards[index])
        index = (index + 1) % cards.size
        return card
    }
}