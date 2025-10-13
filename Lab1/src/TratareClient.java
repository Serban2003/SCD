import java.io.*;
import java.net.Socket;

class TratareClient extends Thread {
    private Socket socket;
    private ServerMultifir server;
    private BufferedReader in;
    private PrintWriter out;
    private int id;
    TratareClient(Socket socket, int id, ServerMultifir server)throws IOException
    {
        this.socket = socket;
        this.server = server;
        in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
        out = new PrintWriter(new BufferedWriter(new OutputStreamWriter( socket.getOutputStream())));
        this.id = id;
        server.broadcast("User ID " + id + " connected!");
    }
    public void run() {
        try {
            while (true) {
                String str = in.readLine();
                if (str.equals("END")) break;
                server.broadcast("Client ID " + id + ": " + str);
            }//.while
            System.out.println("closing...");
        }
        catch(IOException e) {System.err.println("IO Exception");}
        finally {
            try {
                socket.close();
            }catch(IOException e) {System.err.println("Socket not closed");}
        }
    }//.run
    void send(String msg) {
        out.println(msg); // autoFlush already true
    }
}