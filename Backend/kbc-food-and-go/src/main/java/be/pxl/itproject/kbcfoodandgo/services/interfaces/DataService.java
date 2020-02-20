package be.pxl.itproject.kbcfoodandgo.services.interfaces;
import java.io.IOException;

public interface DataService {
    String saveImage(String base64string, String name) throws IOException;
    void deleteImage(String filename);

}
