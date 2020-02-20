package be.pxl.itproject.kbcfoodandgo;

import be.pxl.itproject.kbcfoodandgo.controllers.LogController;
import be.pxl.itproject.kbcfoodandgo.controllers.MealController;
import be.pxl.itproject.kbcfoodandgo.controllers.MenuController;
import be.pxl.itproject.kbcfoodandgo.services.AmazonService;
import be.pxl.itproject.kbcfoodandgo.services.MealManagerImp;
import be.pxl.itproject.kbcfoodandgo.services.MenuManagerImp;
import be.pxl.itproject.kbcfoodandgo.services.PusherServiceImp;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.LogManager;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

@RunWith(SpringRunner.class)
@SpringBootTest
public class KbcFoodAndGoApplicationTests {
    @Autowired
    MealController mealController;
    @Autowired
    LogController logController;
    @Autowired
    MenuController menuController;
    @Autowired
    MealManagerImp mealManagerImp;
    @Autowired
    LogManager logManager;
    @Autowired
    MenuManagerImp menuManagerImp;
    @Autowired
    PusherServiceImp pusherServiceImp;
    @Autowired
    AmazonService amazonService;

    @Test
    public void contextLoads() {
        Assert.assertNotNull(mealController);
        Assert.assertNotNull(logController);
        Assert.assertNotNull(menuController);
        Assert.assertNotNull(mealManagerImp);
        Assert.assertNotNull(logManager);
        Assert.assertNotNull(menuManagerImp);
        Assert.assertNotNull(pusherServiceImp);
        Assert.assertNotNull(amazonService);

    }
}


