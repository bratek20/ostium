plugins {
    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)
}

dependencies {
    api(libs.bratek20.architecture)
    testFixturesApi(testFixtures(libs.bratek20.architecture))

    //TODO-REF generate web layer in app?
    api(libs.bratek20.infrastructure)
    testFixturesApi(testFixtures(libs.bratek20.infrastructure))
    api(libs.spring.web) // for web server to compile

    //TODO-REF generate web layer in app?
    api(libs.bratek20.spring)

    //needed when tests moved to test fixtures, TODO-REF to some plugin
    testFixturesImplementation(libs.junit.jupiter.api)
    testFixturesImplementation(testFixtures(libs.bratek20.architecture))
}

//needed when tests moved to test fixtures, TODO-REF to some plugin
tasks.named("check") {
    dependsOn("testFixturesClasses")
}
tasks.named<Test>("test") {
    dependsOn("testFixturesClasses")
    testClassesDirs += sourceSets["testFixtures"].output.classesDirs
    classpath += sourceSets["testFixtures"].runtimeClasspath
}