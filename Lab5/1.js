$(document).ready(function() {
    let currentDesktop = 1;
    const totalDesktops = 4;
    const desktopWidth = $(window).width();
    
    // show desktop 1 by default
    $('#desktop1').show();
    
    // slide to the next desktop when user clicks on the current desktop
    $('.desktop').click(function() {
        var nextDesktop = currentDesktop+1;
        if (currentDesktop >= totalDesktops) {
            nextDesktop = 1
            currentDesktop = 0
        }
        // slide to the right
        $(this).animate({left: desktopWidth}, 500);
        // slide next desktop to the left
        $(`#desktop${nextDesktop}`).css('left', -desktopWidth).show().animate({left: 0}, 500);
        currentDesktop++;
    });
  });