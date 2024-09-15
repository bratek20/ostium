import com.github.bratek20.architecture.context.spring.SpringContextBuilder
import com.github.bratek20.architecture.serialization.context.SerializationFactory
import com.github.bratek20.infrastructure.httpclient.api.HttpClientConfig
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.logs.api.Logger
import com.github.bratek20.logs.context.Slf4jLogsImpl
import com.github.bratek20.ostium.singlegame.api.GameApi
import com.github.bratek20.ostium.singlegame.context.GameModuleWebClient

fun main(args: Array<String>) {
    var serverUrl = "http://localhost:8080"
    if (args.isNotEmpty()) {
        serverUrl = args[0]
    }

    val c = SpringContextBuilder()
        .withModules(
            HttpClientImpl(),
            GameModuleWebClient(
                config = HttpClientConfig.create(
                    baseUrl = serverUrl
                )
            ),
            Slf4jLogsImpl()
        )
        .build()

    val api = c.get(GameApi::class.java)
    val logger = c.get(Logger::class.java)

    logger.info("Using server URL = $serverUrl")

    logger.info("Starting game...")
    val game = api.startGame()
    val gameAsJson = SerializationFactory.createSerializer()
        .serialize(game)
        .getValue()

    logger.info("Game = $gameAsJson")
}