module.exports = (sequelize, Sequelize) => {
    const MovieCountries = sequelize.define("movieCountries")

    return MovieCountries
}