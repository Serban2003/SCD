import java.net.*;
import java.io.*;

public class ServerSimplu {
    public static void main(String[] args) throws IOException{
        ServerSocket ss=null;
        Socket socket=null;
        try{
            String line = "";
            ss = new ServerSocket(1900); //crează obiectul serversocket
            System.out.println("Waiting for response...");
            socket = ss.accept(); //incepe aşteptarea pe portul 1900

            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            PrintWriter out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())),true);
            BufferedReader outClient = new BufferedReader(new InputStreamReader(System.in));

            while(!line.equals("END")){
                line = in.readLine(); //citeste datele de la client
                System.out.println("Received message: " + line);
                System.out.print("Response: ");
                String message = outClient.readLine();
                out.println(message);
            }

        }catch(Exception e){e.printStackTrace();}
        finally{
            if (ss != null) {
                ss.close();
            }
            if(socket!=null) socket.close();
        }
    }
}