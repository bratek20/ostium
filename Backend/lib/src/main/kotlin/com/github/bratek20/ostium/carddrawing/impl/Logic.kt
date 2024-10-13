package com.github.bratek20.ostium.carddrawing.impl

import com.github.bratek20.ostium.carddrawing.api.*

import com.github.bratek20.ostium.kaijugame.api.*

class CardDrawerLogic: CardDrawer {
    override fun draw(): Card {
        return Card.create(
            type = randomType(),
            focusCost = randomFocusCost(),
            value = randomValue()
        )
    }

    private fun randomType(): DamageType {
        return DamageType.entries.random()
    }

    private fun randomFocusCost(): Int {
        return (1..5).random()
    }

    private fun randomValue(): Int {
        return (1..5).random()
    }
}

class CardDrawerFactoryLogic: CardDrawerFactory {
    override fun create(): CardDrawer {
        return CardDrawerLogic()
    }
}