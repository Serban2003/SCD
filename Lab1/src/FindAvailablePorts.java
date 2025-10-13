import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;

public class FindAvailablePorts {
    public static void main(String[] args) throws UnknownHostException {
        InetAddress address = InetAddress.getByName("localhost");
        ArrayList<Integer> availablePorts = new ArrayList<Integer>();
        int numberPorts = (int) Math.pow(2, 16);
        for(int i = 0; i < numberPorts; ++i){
            try{
                Socket socket = new Socket(address, i);
                availablePorts.add(i);
                socket.close();
            } catch (IOException e) {

            }
        }

        System.out.println("Available ports: " + availablePorts);
    }
}