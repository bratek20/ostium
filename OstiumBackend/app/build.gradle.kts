plugins {
    alias(libs.plugins.bratek20.spring.web.app.conventions)
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))
}