plugins {
    id("org.springframework.boot") version "2.7.18"

    alias(libs.plugins.bratek20.internal.kotlin.library.conventions)
}

dependencies {
    implementation(project(":lib"))
    testImplementation(testFixtures(project(":lib")))

    //implementation(platform(libs.spring.boot.dependencies))
    implementation("org.springframework.boot:spring-boot-starter-web:2.7.18")
    implementation("org.springdoc:springdoc-openapi-ui:1.6.9")
}