import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class TimeServer extends Thread{
    boolean running = true;
    public TimeServer() { start();}

    public void run(){
        try{
            DatagramSocket socket = new DatagramSocket(1977);
            while(running){
                //Wait for client
                byte[] buf = new byte[256];
                DatagramPacket packet = new DatagramPacket(buf, buf.length);
                // Waiting for connections...
                System.out.println("Waiting for messages...");
                socket.receive(packet);

                //Read address and port
                InetAddress address = packet.getAddress();
                int port = packet.getPort();
                String receivedMessage = new String(packet.getData(), 0, packet.getLength());
                System.out.println("Received message: " + receivedMessage);
                //Send a reply to client

                BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
                System.out.print("Send message to client: ");
                String message = reader.readLine();
                buf = message.getBytes();
                packet = new DatagramPacket(buf, buf.length, address, port);
                socket.send(packet);

            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
}
    public static void main(String[] args) {
        TimeServer timeServer1 = new TimeServer();
    }
}
