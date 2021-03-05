<template>
    <div class="container">
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container">
                    <a class="navbar-brand">
                        <img src="./assets/logo.png" height="30" object-fit="cover" style="vertical-align:middle" />
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
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
    }
</style>
