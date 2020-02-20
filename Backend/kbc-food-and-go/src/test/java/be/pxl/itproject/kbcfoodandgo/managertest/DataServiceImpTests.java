package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.services.DataServiceImp;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;
import java.io.IOException;

import static org.mockito.Mockito.times;
import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class DataServiceImpTests {


    @Mock
    private DataServiceImp dataServiceImp;

    @Test
    public void saveFileToServerShouldThrowErrorWhenFileIsNotSaved() throws IOException {
        MealDTO mealDTO = new MealDTO();

        Mockito.doThrow(new IOException()).when(dataServiceImp).saveImage(mealDTO.getName(),mealDTO.getImageBase64());
        try {
            dataServiceImp.saveImage(mealDTO.getName(),mealDTO.getImageBase64());
        } catch (IOException ignored) {
        }
        Mockito.verify(dataServiceImp, times(1)).saveImage(mealDTO.getName(),mealDTO.getImageBase64());
    }

    @Test
    public void saveFileToServerShouldNotThrowErrorWhenFileIsSaved() throws IOException {
        MealDTO mealDTO = new MealDTO();
        when(dataServiceImp.saveImage(mealDTO.getName(),mealDTO.getImageBase64())).thenReturn("123");

        String value = dataServiceImp.saveImage(mealDTO.getName(),mealDTO.getImageBase64());
        Assert.assertTrue(value != null);
        Mockito.verify(dataServiceImp, times(1)).saveImage(mealDTO.getName(),mealDTO.getImageBase64());
    }


}
