package be.pxl.itproject.kbcfoodandgo.aop;

import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.LogManager;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MealManager;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.springframework.context.annotation.Configuration;

import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Optional;

@Aspect
@Configuration
public class AdminLoggerAspect {

    private final
    LogManager logManager;
    private final
    MealManager mealManager;

    public AdminLoggerAspect(LogManager logManager, MealManager mealManager) {
        this.logManager = logManager;
        this.mealManager = mealManager;
    }

    @Around("@annotation(be.pxl.itproject.kbcfoodandgo.aop.annotations.LogAction)")
    public Object logGetActivity(ProceedingJoinPoint joinPoint) throws Throwable {
        Date logDate = new Date();
        joinPoint.getSignature();
        String methodName = joinPoint.getSignature().toShortString();
        List<Object> methodParameter = Arrays.asList(joinPoint.getArgs());
        String mealName = null;
        Optional<Meal> mealOptional;
        switch (methodName) {
            case "MealController.getAllMeals()":
                logManager.addLog(new Log("De administrator heeft alle maaltijden opgevraagd", logDate));
                break;

            case "MealController.getMealById(..)":
                mealOptional = mealManager.getMealById((Long) methodParameter.get(0));
                if (mealOptional.isPresent()) {
                    mealName = mealOptional.get().getName();
                }

                if (mealName != null) {
                    logManager.addLog(new Log(String.format("De administrator heeft de maaltijd:  \"%s\" opgevraagd", mealName), logDate));
                } else {
                    logManager.addLog(new Log(String.format("De administrator heeft geprobeerd om een niet bestaande maaltijd met id \"%d\"", ((Long) methodParameter.get(0)).intValue()), logDate));
                }
                break;

            case "MealController.addMeal(..)":
                MealDTO meal = (MealDTO) methodParameter.get(0);
                logManager.addLog(new Log(String.format("De administrator heeft de maaltijd: %s toegevoegd", meal.toString()), logDate));
                break;

            case "MealController.deleteMeal(..)":
                mealManager.getMealById((Long) methodParameter.get(0));
                logManager.addLog(new Log("Error maaltijd bestaat niet", logDate));
                break;

            case "UserController.create(..)":
                logManager.addLog(new Log("Nieuwe gebruiker aangemaakt", logDate));
                break;

            default:
                logManager.addLog(new Log(String.format("Admin heeft de methode %s met de parameter %s uitgevoerd", methodName, methodParameter), logDate));
        }
        return joinPoint.proceed();
    }
}
