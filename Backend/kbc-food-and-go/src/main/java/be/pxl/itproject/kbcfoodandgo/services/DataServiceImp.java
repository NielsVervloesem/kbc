package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.services.interfaces.DataService;
import com.google.common.hash.Hashing;
import org.apache.commons.io.FileUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.Base64;

@Service
public class DataServiceImp implements DataService {

    private AmazonService amazonClient;

    @Autowired
    public DataServiceImp(AmazonService amazonService){
        this.amazonClient = amazonService;
    }

    @Override
    public String saveImage(String base64string, String name) throws IOException {
        String sha256hex = Hashing.sha256()
                .hashString(name, StandardCharsets.UTF_8)
                .toString() + ".jpg";

        File file = new File(sha256hex);
        byte[] decodedBytes = Base64.getDecoder().decode(base64string);
        FileUtils.writeByteArrayToFile(file, decodedBytes);
        this.amazonClient.uploadFileTos3bucket(sha256hex, file);
        Files.delete(file.toPath());
        return sha256hex;
    }

    @Override
    public void deleteImage(String filename) {
        this.amazonClient.deleteFileFroms3bucket(filename);
    }
}
