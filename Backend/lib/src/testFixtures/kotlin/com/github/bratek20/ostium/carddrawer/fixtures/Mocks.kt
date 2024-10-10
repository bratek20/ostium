package com.github.bratek20.ostium.carddrawer.fixtures

import com.github.bratek20.ostium.carddrawer.api.CardDrawerApi
import com.github.bratek20.ostium.kajiugame.api.Card
import com.github.bratek20.ostium.kajiugame.fixtures.CardDef
import com.github.bratek20.ostium.kajiugame.fixtures.card

class CardDrawerApiMock: CardDrawerApi {
    private var cards: List<CardDef.() -> Unit> = listOf({})

    fun setCards(cards: List<CardDef.() -> Unit>) {
        this.cards = cards
    }

    private var index = 0
    override fun draw(): Card {
        val card = card(cards[index])
        index = (index + 1) % cards.size
        return card
    }
}