import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import java.util.Objects;

public class ClientSimplu2 {
    public static void main(String[] args)throws Exception{
        Socket socket=null;
        try {
            InetAddress address = InetAddress.getByName("localhost");
            //se putea utiliza varianta alternativă: InetAddress.getByName("127.0.0.1")
            socket = new Socket(address,1900);

            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            // Output is automatically flushed by PrintWriter:
            PrintWriter out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())),true);

            BufferedReader inClient = new BufferedReader(new InputStreamReader(System.in));
            String message;
            // Reading data using readLine
            do{
                System.out.print("Message: ");
                message = inClient.readLine();
                out.println(message);
            }while(!Objects.equals(message, "END"));

            out.println("END"); //trimite mesaj care determină serverul să închidă conexiunea
        }
        catch (Exception ex) {ex.printStackTrace();}
        finally{
            if (socket != null) {
                socket.close();
            }
        }
    }
}