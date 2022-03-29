// Forms Demo
// ----------------------------------- 


(function(window, document, $, undefined){

  $(function(){

    // BOOTSTRAP SLIDER CTRL
    // ----------------------------------- 
      try{
          $('[data-ui-slider]').slider();
     
    // CHOSEN
    // ----------------------------------- 

    $('.chosen-select').chosen();

    // MASKED
    // ----------------------------------- 

    $('[data-masked]').inputmask();

    // FILESTYLE
    // ----------------------------------- 

    $('.filestyle').filestyle();

    // WYSIWYG
    // ----------------------------------- 

    $('.wysiwyg').wysiwyg();

      }
      catch (ex) {

      }
    // DATETIMEPICKER
    // ----------------------------------- 

    $('#datetimepicker1').datetimepicker({
      icons: {
          time: 'fa fa-clock-o',
          date: 'fa fa-calendar',
          up: 'fa fa-chevron-up',
          down: 'fa fa-chevron-down',
          previous: 'fa fa-chevron-left',
          next: 'fa fa-chevron-right',
          today: 'fa fa-crosshairs',
          clear: 'fa fa-trash'         
        }
    });



       $('#datetimepicker1_1').datetimepicker({
           icons: {
               time: 'fa fa-clock-o',
               date: 'fa fa-calendar',
               up: 'fa fa-chevron-up',
               down: 'fa fa-chevron-down',
               previous: 'fa fa-chevron-left',
               next: 'fa fa-chevron-right',
               today: 'fa fa-crosshairs',
               clear: 'fa fa-trash'
           }
    });

       $('#datetimepicker1_2').datetimepicker({
           icons: {
               time: 'fa fa-clock-o',
               date: 'fa fa-calendar',
               up: 'fa fa-chevron-up',
               down: 'fa fa-chevron-down',
               previous: 'fa fa-chevron-left',
               next: 'fa fa-chevron-right',
               today: 'fa fa-crosshairs',
               clear: 'fa fa-trash'
           }
       });

       $('#datetimepicker1_3').datetimepicker({
           icons: {
               time: 'fa fa-clock-o',
               date: 'fa fa-calendar',
               up: 'fa fa-chevron-up',
               down: 'fa fa-chevron-down',
               previous: 'fa fa-chevron-left',
               next: 'fa fa-chevron-right',
               today: 'fa fa-crosshairs',
               clear: 'fa fa-trash'
           }
       });


       $('#datetimepicker1_4').datetimepicker({
           icons: {
               time: 'fa fa-clock-o',
               date: 'fa fa-calendar',
               up: 'fa fa-chevron-up',
               down: 'fa fa-chevron-down',
               previous: 'fa fa-chevron-left',
               next: 'fa fa-chevron-right',
               today: 'fa fa-crosshairs',
               clear: 'fa fa-trash'
           }
       });

    // only time
    $('#datetimepicker2').datetimepicker({
        format: 'LT'
      // viewMode: 'years',
      //  format: 'MM/YYYY'
    });

  });

})(window, document, window.jQuery);