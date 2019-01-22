(function ($) {
    $(document).ready(function () {
        $('select').chosen();
        var windowsHeight = $(document).height();
        var htmlHeight = $('html').height();
        var footer = $('footer');
        console.log(windowsHeight);
        console.log(htmlHeight);
        if (windowsHeight-1 <= htmlHeight) {
            footer.css('position', 'absolute');
            footer.css('bottom', '0px');
        }

        


    });
    


})(jQuery);