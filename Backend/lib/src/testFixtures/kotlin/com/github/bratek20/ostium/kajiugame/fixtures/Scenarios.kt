package com.github.bratek20.ostium.kajiugame.fixtures

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.ostium.carddrawer.fixtures.CardDrawerApiMock
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.kajiugame.api.GameApi

class KaijuGameScenarios(
    private val api: GameApi,
    private val gamesManagementApi: GamesManagementApi,
    private val cardDrawerApiMock: CardDrawerApiMock
) {
}

class KaijuGameScenariosModule: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setClass(KaijuGameScenarios::class.java)
    }

}