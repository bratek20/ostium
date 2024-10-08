// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.user.api

data class UserData(
    private var name: String,
) {
    fun getName(): Username {
        return Username(this.name)
    }

    fun setName(name: Username) {
        this.name = name.value
    }

    companion object {
        fun create(
            name: Username,
        ): UserData {
            return UserData(
                name = name.value,
            )
        }
    }

    fun update(other: UserData) {
        this.name = other.name
    }
}