import socket

def get_paddle_position(ball_y):
    paddle_y = ball_y  # AI logic: follow the ball's Y-axis
    return paddle_y

def start_server():
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind(('localhost', 8080))
    server.listen(1)
    while True:
        conn, addr = server.accept()
        ball_y = float(conn.recv(1024).decode())  # Receive ball Y position from C#
        paddle_y = get_paddle_position(ball_y)
        conn.send(str(paddle_y).encode())  # Send paddle position back to C#
        conn.close()

if __name__ == "__main__":
    start_server()
