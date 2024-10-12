#include <iostream>
#include <SFML/Network.hpp>

sf::Vector2f ballSpeed(3.0f, 3.0f);

sf::Vector2f updateBallPosition(sf::Vector2f ballPosition)
{
    // Update ball position
    ballPosition += ballSpeed;

    // Collision with screen bounds
    if (ballPosition.x < 0 || ballPosition.x > 800)  // Screen width
        ballSpeed.x = -ballSpeed.x;
    if (ballPosition.y < 0 || ballPosition.y > 600)  // Screen height
        ballSpeed.y = -ballSpeed.y;

    return ballPosition;
}

int main()
{
    sf::TcpListener listener;
    listener.listen(8081);
    sf::TcpSocket client;
    listener.accept(client);

    while (true)
    {
        char data[128];
        std::size_t received;
        client.receive(data, 128, received);

        sf::Vector2f ballPosition = updateBallPosition(sf::Vector2f(atof(data), atof(data + 64)));
        std::string response = std::to_string(ballPosition.x) + "," + std::to_string(ballPosition.y);

        client.send(response.c_str(), response.size());
    }

    return 0;
}
