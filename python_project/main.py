import kivy
from kivy.app import App
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.button import Button
from kivy.uix.label import Label
from kivy.uix.popup import Popup
from kivy.clock import Clock
from datetime import datetime

class SaudiGame(App):
    def build(self):
        self.title = 'لعبة سعودية - Saudi Game'
        self.power = 0
        
        # الواجهة الرئيسية
        layout = BoxLayout(orientation='vertical', padding=20, spacing=10)
        
        self.label_title = Label(
            text='مرحباً بك في اللعبة السعودية', 
            font_size='24sp',
            bold=True
        )
        
        self.label_power = Label(
            text=f'القوة الحالية: {self.power}', 
            font_size='20sp'
        )
        
        btn_collect = Button(
            text='اجمع القوة ⚡', 
            size_hint=(1, 0.2),
            background_color=(0, 0.6, 0, 1) # أخضر سعودي
        )
        btn_collect.bind(on_press=self.collect_power)
        
        btn_check_event = Button(
            text='تحقق من المناسبات 🇸🇦', 
            size_hint=(1, 0.2)
        )
        btn_check_event.bind(on_press=self.check_saudi_events)
        
        layout.add_widget(self.label_title)
        layout.add_widget(self.label_power)
        layout.add_widget(self.btn_saudi_flag())
        layout.add_widget(btn_collect)
        layout.add_widget(btn_check_event)
        
        # فحص تلقائي عند التشغيل
        Clock.schedule_once(lambda dt: self.check_saudi_events(None), 1)
        
        return layout

    def btn_saudi_flag(self):
        # تمثيل بسيط للعلم السعودي
        return Label(text='🇸🇦 🇸🇦 🇸🇦', font_size='40sp')

    def collect_power(self, instance):
        self.power += 10
        self.label_power.text = f'القوة الحالية: {self.power}'

    def check_saudi_events(self, instance):
        today = datetime.now()
        msg = ""
        
        # الخميس
        if today.weekday() == 3: # الخميس هو 3 في بايثون (الاثنين 0)
            msg = "اليوم الخميس! حصلت على مكافأة أسبوعية +100 قوة."
            self.power += 100
        
        # اليوم الوطني - 23 سبتمبر
        elif today.month == 9 and today.day == 23:
            msg = "كل عام والمملكة بخير! اليوم الوطني السعودي. هدية +1000 قوة."
            self.power += 1000
            
        # يوم التأسيس - 22 فبراير
        elif today.month == 2 and today.day == 22:
            msg = "يوم التأسيس! فخورين بجذورنا. مكافأة +500 قوة."
            self.power += 500
            
        # التاريخ الذي طلبه المستخدم (20 مارس 2026)
        elif today.year == 2026 and today.month == 3 and today.day == 20:
            msg = "تم منحك حزمة العيد وقوة خمسة آلاف (5000)."
            self.power += 5000
        else:
            msg = "لا توجد مناسبات وطنية اليوم. استمر في اللعب!"

        self.label_power.text = f'القوة الحالية: {self.power}'
        
        # إظهار رسالة منبثقة (Popup)
        popup = Popup(
            title='تنبيه المناسبات',
            content=Label(text=msg, halign='center'),
            size_hint=(0.8, 0.4)
        )
        popup.open()

if __name__ == '__main__':
    SaudiGame().run()
