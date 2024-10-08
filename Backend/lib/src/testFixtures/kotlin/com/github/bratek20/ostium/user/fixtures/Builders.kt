// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.user.fixtures

import com.github.bratek20.ostium.user.api.*

fun username(value: String = "someValue"): Username {
    return Username(value)
}

data class UserDataDef(
    var name: String = "someValue",
)
fun userData(init: UserDataDef.() -> Unit = {}): UserData {
    val def = UserDataDef().apply(init)
    return UserData.create(
        name = Username(def.name),
    )
}