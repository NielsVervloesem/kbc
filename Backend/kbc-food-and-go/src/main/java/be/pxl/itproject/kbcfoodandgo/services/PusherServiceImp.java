package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.PusherService;
import com.pusher.rest.Pusher;
import org.springframework.stereotype.Component;
import java.util.Collections;

@Component
public class PusherServiceImp implements PusherService {
    private Pusher pusher;

    public PusherServiceImp() {
        pusher = new Pusher("893926", "3e81de0297856b72df54", "0b474ed73c4beb6fd7fb");
        pusher.setCluster("eu");
        pusher.setEncrypted(true);
    }

    public Pusher getPusher() {
        return pusher;
    }

    public void setPusher(Pusher pusher) {
        this.pusher = pusher;
    }

    @Override
    public void onTestLog(Log log) {
        pusher.trigger("logChannel", "logEvent", Collections.singletonMap("log", log));
    }

    @Override
    public void onCreateMenu() {
        pusher.trigger("menuChannel", "createMenu", ("menu"));
    }


    @Override
    public void onAddMealHistory() {
        pusher.trigger("mealHistory", "mealHistory", ("mealHistory"));
    }
}
