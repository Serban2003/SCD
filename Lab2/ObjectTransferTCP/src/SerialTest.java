import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

public class SerialTest extends Thread{
    public void run(){
        try{
            ServerSocket ss = new ServerSocket(1977);
            System.out.println("Waiting for connections...");
            Socket s = ss.accept();
            ObjectInputStream ois = new ObjectInputStream(s.getInputStream());
            //Person p = (Person) ois.readObject();
            //System.out.println(p);

            CustomThread th = (CustomThread) ois.readObject();
            th.start();

            s.close();
            ss.close();
        }
        catch(Exception e){
            e.printStackTrace();
        }
    }
    public static void main(String[] args) throws Exception{
        (new SerialTest()).start();
        Socket s = new Socket(InetAddress.getByName("localhost"),1977);
        ObjectOutputStream oos = new ObjectOutputStream(s.getOutputStream());
        //Person p = new Person("Alin",14);
        //oos.writeObject(p);

        CustomThread th = new CustomThread();
        oos.writeObject(th);
        s.close();
    }
}
