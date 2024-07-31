plugins {
    alias(libs.plugins.bratek20.simple.app.conventions)
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))

    implementation(libs.bratek20.logs)
}