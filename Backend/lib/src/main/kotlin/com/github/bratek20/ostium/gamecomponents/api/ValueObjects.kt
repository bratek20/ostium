// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamecomponents.api

data class GateDurabilityMarker(
    val value: Int
) {
    override fun toString(): String {
        return value.toString()
    }

    operator fun plus(other: GateDurabilityMarker): GateDurabilityMarker {
        return GateDurabilityMarker(this.value + other.value)
    }

    operator fun minus(other: GateDurabilityMarker): GateDurabilityMarker {
        return GateDurabilityMarker(this.value - other.value)
    }

    operator fun times(other: GateDurabilityMarker): GateDurabilityMarker {
        return GateDurabilityMarker(this.value * other.value)
    }
}

data class CreatureCardId(
    val value: String
) {
    override fun toString(): String {
        return value.toString()
    }
}

data class CreatureCard(
    private val id: String,
) {
    fun getId(): CreatureCardId {
        return CreatureCardId(this.id)
    }

    companion object {
        fun create(
            id: CreatureCardId,
        ): CreatureCard {
            return CreatureCard(
                id = id.value,
            )
        }
    }
}

data class GateCard(
    private val destroyed: Boolean,
) {
    fun getDestroyed(): Boolean {
        return this.destroyed
    }

    companion object {
        fun create(
            destroyed: Boolean,
        ): GateCard {
            return GateCard(
                destroyed = destroyed,
            )
        }
    }
}

data class GateDurabilityCard(
    private val myMarker: Int,
    private val opponentMarker: Int,
) {
    fun getMyMarker(): GateDurabilityMarker {
        return GateDurabilityMarker(this.myMarker)
    }

    fun getOpponentMarker(): GateDurabilityMarker {
        return GateDurabilityMarker(this.opponentMarker)
    }

    companion object {
        fun create(
            myMarker: GateDurabilityMarker,
            opponentMarker: GateDurabilityMarker,
        ): GateDurabilityCard {
            return GateDurabilityCard(
                myMarker = myMarker.value,
                opponentMarker = opponentMarker.value,
            )
        }
    }
}