import java.io.*;
import java.net.*;
import java.util.ArrayList;
import java.util.HashMap;

public class ServerMultifir {
    public static final int PORT = 1900;
    public int currentID = 0;
    public ArrayList<TratareClient> clientList;
    void startServer() {
        ServerSocket ss = null;
        clientList = new ArrayList<>();
        try {
            ss = new ServerSocket(PORT);
            while (true) {
                Socket socket = ss.accept();
                TratareClient client = new TratareClient(socket, ++currentID, this);
                clientList.add(client);
                client.run();
            }
        }catch(IOException ex)
        {
            System.err.println("Eroare :"+ex.getMessage());
        }
        finally {
            try {
                if (ss != null) {
                    ss.close();
                }
            }
            catch(IOException ex2) { }
        }
    }
    void broadcast(String msg) {
        for (TratareClient c : clientList) {
            c.send(msg);
        }
    }


    public static void main(String args[]) {
        ServerMultifir smf = new ServerMultifir();
        smf.startServer();
    }
}
