const osmosis = require('osmosis');
const urls = require('./../config/urls.js')

exports.getRatings = (cpUserId, sucess, error) => {
    let urlBase = `${urls.base}${urls.user}${cpUserId}${urls.ratings}page=`
    let url = ''

    osmosis
        .get(urlBase)
        .find('#main > div.container.py-5 > div > article > div > div > div > div > section > nav > ul > li:nth-child(12) > a')
        .set({'pages': '@href'})
        .data(listing => {
            const pages = 9//listing.pages.split('&')[3].replace('page=','')
            for (let j = 0; j <= pages; j++) {
                url = urlBase + j
                for (let i = 1; i <= 100; i++) {
                    osmosis
                        .get(url)
                        .set({'score': '#score-table > tbody > tr:nth-child(' + i + ') > td:nth-child(3) > div > div'})
                        .find('#score-table > tbody > tr:nth-child(' + i + ') > td:nth-child(1) > div > div > a')
                        .follow('@href')
                        .find('#secao-filme-webdoor > div > div > div > div.col-lg-8.text-md-left.mb-3.mb-lg-0 > div.movie-user-actions > ul > li.nav-item.dropdown > div > a:nth-child(1)')
                        .set({'cpMovieId': '@href'})
                        .data(listing => {
                            const cpMovieId = listing.cpMovieId.split('/')[5].replace('criar?movie=','')
                            const score = listing.score 
                            const rating = ({cpUserId, cpMovieId, score})

                            sucess(rating)
                        })
                        .error(error)
                }
            }
        })
}

exports.getWatchlist = (cpUserId, sucess, error) => {
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

            const movie = {...listing, cpMovieId, year, countries, minutes}

            sucess(movie)
        })
        .error(error)
}

exports.getUser = (cpUsername, sucess, error) => {
    const url = `${urls.base}${cpUsername}`

    osmosis
        .get(url)
        .set({
            'name': '#friend_ticker > h2',
            'picture': '#secao-filme-webdoor > div > div > div > div.col-sm-auto.align-self-start > a > div > img @src'
        })
        .data(listing => {
            const cpUserId = listing.picture.split('/')[10]

            if (!+cpUserId)
              error()
      
            const user = {...listing, cpUsername, cpUserId}

            sucess(user)
        })
        .error(error)
}


