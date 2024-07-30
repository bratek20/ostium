plugins {
    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)

    id("com.github.johnrengelman.shadow") version "7.1.2"
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))

    implementation(libs.bratek20.logs)
}

tasks.withType<com.github.jengelman.gradle.plugins.shadow.tasks.ShadowJar> {
    archiveClassifier.set("") // optional, removes the "-all" suffix from the JAR name
    manifest {
        attributes["Main-Class"] = "OstiumWebAppTesterKt" // Replace with your main class
    }
}

tasks.build {
    dependsOn(tasks.shadowJar)
}