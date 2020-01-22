const osmosis = require('osmosis');
const urls = require('./../config/urls.js')

exports.getWatchlist = (cpUserId) => {
    const url = `${urls.base}${urls.user}${cpUserId}${urls.watchList}page=0`

    osmosis
        .get(url)
        .find('#main > div.container.py-5 > div > article > div > div > div > div > section > div > div > a')
        .set({'id': '@href'})
        .follow('@href')
        .set({
            'title': '#secao-filme-webdoor > div > div > div > div.col-lg-8.text-md-left.mb-3.mb-lg-0 > h2',
            'year': '#secao-filme-webdoor > div > div > div > div.col-lg-8.text-md-left.mb-3.mb-lg-0 > div.font-weight-bold.font-italic',
            'directors': [
                '#main > div.container.py-5 > div > article > div.row > div.col-md-4.mb-4 > div > div > div > section > div.col-sm-6.col-md-12.px-2.py-4 > dl > dd:nth-child(2) > a'
            ],
            'screenwriters': [
                '#main > div.container.py-5 > div > article > div.row > div.col-md-4.mb-4 > div > div > div > section > div.col-sm-6.col-md-12.px-2.py-4 > dl > dd:nth-child(4) > a'
            ],
            'genres': [
                '#main > div.container.py-5 > div > article > div.row > div.col-md-4.mb-4 > div > div > div > section > div.col-sm-6.col-md-12.px-2.py-4 > dl > dd:nth-child(6) > a'
            ],
            'countries': '#main > div.container.py-5 > div > article > div.row > div.col-md-4.mb-4 > div > div > div > section > div.col-sm-6.col-md-12.px-2.py-4 > dl > dd:nth-child(8)',
            'minutes': '#main > div.container.py-5 > div > article > div.row > div.col-md-4.mb-4 > div > div > div > section > div.col-sm-6.col-md-12.px-2.py-4 > dl > dd:nth-child(10)',
            'cast': [
                '#main > div.container.py-5 > div > article > div.row > div.col-md-8 > div.panel-pane.pane-entity-view.pane-node > div > section.section-cast.mb-4 > dl > dt > a'
            ],
            'publishersAverage': '#secao-notas-cineplayers > ul > li:nth-child(7) > div > div.col-auto > div',
            'usersAverage': '#secao-filme-webdoor > div > div > div > div.movie-rates-column.col-lg > div > div > div > div:nth-child(1) > div.rate-bubble.mb-1 > div'
        })
        .data(listing => {
            const cpMovieId = listing.id.replace('/filmes/','')
            const year = listing.year.substr(listing.year.length - 5, 4)
            const countries = listing.countries.trim().split(', ')
            const minutes = listing.minutes.match( /\d+/g )[0]

            movie = {...listing, cpMovieId, year, countries, minutes}

            console.log(movie)
            //save on db
        })
        .error(console.log)
        //.done() send email
}

exports.getUser = async (cpUserId) => {
    const url = `${urls.base}${urls.user}${cpUserId}`
    let user = {}
    
    await osmosis
        .get(url)
        .set({
            'name': '#friend_ticker > h2',
            'picture': '#secao-filme-webdoor > div > div > div > div.col-sm-auto.align-self-start > a > div > img @src'
        })
        .data(listing => user = listing)
        
    return user
}






