plugins {
    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))
}