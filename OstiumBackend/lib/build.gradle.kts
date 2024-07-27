plugins {
    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)
}

dependencies {
    api(libs.bratek20.architecture)
    testApi(testFixtures(libs.bratek20.architecture))

    api(libs.bratek20.logs)
    testApi(testFixtures(libs.bratek20.logs.logback))
}