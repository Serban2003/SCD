import java.io.Serializable;

public class CustomThread extends Thread implements Serializable {
    @Override
    public void run() {
        for(int i = 0; i < 10; ++i){
            System.out.println("Pasul " + i);
        }
    }
}
