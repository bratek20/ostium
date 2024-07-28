plugins {
    id("org.springframework.boot") version "2.7.18"
    id("io.spring.dependency-management") version "1.0.11.RELEASE"

    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))


    implementation("org.springframework.boot:spring-boot-starter-web")
    implementation(libs.spring.boot.swagger)
}