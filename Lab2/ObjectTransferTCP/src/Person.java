import java.io.Serializable;

class Person implements Serializable {
    String name;
    int age;
    Person(String name, int age){
        this.name = name; this.age = age;
    }
    public String toString(){
        return "Person: " + name + ", age: " + age;
    }
}