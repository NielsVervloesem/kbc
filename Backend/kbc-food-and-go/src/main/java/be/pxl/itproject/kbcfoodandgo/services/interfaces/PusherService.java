package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;

public interface PusherService {
    void onTestLog(Log log);
    void onCreateMenu();
    void onAddMealHistory();
}
