// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesetup.api

import com.github.bratek20.ostium.gamecomponents.api.*

interface GameSetupApi {
    fun startGame(): Game

    fun playCard(cardId: CreatureCardId, row: RowType): Game

    fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType): Game
}