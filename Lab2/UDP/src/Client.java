import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.UnknownHostException;

public class Client extends Thread{
    boolean running = true;
    InetAddress address;
    int port;

    public Client() throws UnknownHostException {
        address = InetAddress.getByName("localhost");
        port = 1977;
        start();
    }
    public void run() {
        try{
            DatagramSocket socket = new DatagramSocket();
            while(running){
                BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
                System.out.print("Send message to server: ");
                String message = reader.readLine();
                byte [] buf = message.getBytes();
                DatagramPacket packet = new DatagramPacket(buf, buf.length, address, port);
                socket.send(packet);

                buf = new byte[256];
                packet = new DatagramPacket(buf, buf.length);
                System.out.println("Waiting for messages...");
                socket.receive(packet);
                String receivedMessage = new String(packet.getData(),0, packet.getLength());

                System.out.println("Received message: " + receivedMessage);
            }
        }catch(Exception ex){ex.printStackTrace();}
    }

    public static void main(String[] args) throws UnknownHostException {
        Client cliet = new Client();
    }
}