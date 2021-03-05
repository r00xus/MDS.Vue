<template>
    <div class="container">
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container">
                    <a class="navbar-brand">
                        <router-link :to="{ name: 'directions' }">
                            <img src="./assets/logo.png" height="30" object-fit="cover" style="vertical-align:middle" />
                        </router-link>
                        <span style="font-weight:bold">
                            METINVEST
                        </span>
                        <span>
                            Digital
                        </span>
                    </a>

                    <h3>Справочник управлений</h3>

                </div>
            </nav>
        </header>
        <router-view />
    </div>
</template>

<script>
    export default {
        name: 'App',
    }
</script>

<style>
    #app {
        font-family: Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
    }

    .error {
        color: red;
    }
</style>
